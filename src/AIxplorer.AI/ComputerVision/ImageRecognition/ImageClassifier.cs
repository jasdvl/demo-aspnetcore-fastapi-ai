using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace AIxplorer.AI.ComputerVision.ImageRecognition
{
    public class ImageClassifier
    {
        private readonly string _modelFilePath;

        private readonly Lazy<InferenceSession> LazySession;

        private InferenceSession Session => LazySession.Value;

        public ImageClassifier(string modelFilePath)
        {
            _modelFilePath = modelFilePath;
            LazySession = new(() => new InferenceSession(_modelFilePath));
        }

        public IEnumerable<Prediction> ClassifyImage(Image<Rgb24> image)
        {
            image.Mutate(x =>
            {
                x.Resize(new ResizeOptions
                {
                    Size = new Size(224, 224),
                    Mode = ResizeMode.Crop
                });
            });

            // Preprocess image
            Tensor<float> input = PreprocessImage(image);

            // Setup inputs
            var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("data", input)
            };

            // Run inference

            using IDisposableReadOnlyCollection<DisposableNamedOnnxValue> results = Session.Run(inputs);

            // Postprocess to get softmax vector
            IEnumerable<Prediction> top10 = PostProcess(results);

            return top10;
        }

        private Tensor<float> PreprocessImage(Image<Rgb24> image)
        {
            Tensor<float> input = new DenseTensor<float>(new[] { 1, 3, 224, 224 });
            var mean = new[] { 0.485f, 0.456f, 0.406f };
            var stddev = new[] { 0.229f, 0.224f, 0.225f };
            image.ProcessPixelRows(accessor =>
            {
                for (int y = 0; y < accessor.Height; y++)
                {
                    Span<Rgb24> pixelSpan = accessor.GetRowSpan(y);
                    for (int x = 0; x < accessor.Width; x++)
                    {
                        input[0, 0, y, x] = ((pixelSpan[x].R / 255f) - mean[0]) / stddev[0];
                        input[0, 1, y, x] = ((pixelSpan[x].G / 255f) - mean[1]) / stddev[1];
                        input[0, 2, y, x] = ((pixelSpan[x].B / 255f) - mean[2]) / stddev[2];
                    }
                }
            });

            return input;
        }

        private IEnumerable<Prediction> PostProcess(IDisposableReadOnlyCollection<DisposableNamedOnnxValue> results)
        {
            IEnumerable<float> output = results.First().AsEnumerable<float>();
            float sum = output.Sum(x => (float) Math.Exp(x));
            IEnumerable<float> softmax = output.Select(x => (float) Math.Exp(x) / sum);

            // Extract top 10 predicted classes
            IEnumerable<Prediction> top10 = softmax.Select((x, i) => new Prediction { Label = LabelMap.Labels[i], Confidence = x })
                               .OrderByDescending(x => x.Confidence)
                               .Take(10);

            return top10;
        }
    }
}
