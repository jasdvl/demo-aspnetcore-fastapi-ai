from app.utilities.image_utils import ImageUtils
from diffusers import StableDiffusion3Pipeline
from numpy import double
import torch
from PIL import Image
from io import BytesIO
import base64

class ImageGenerationService:
    def __init__(self, model_path: str):
        """
        Initializes the ImageGenerator with a specific Stable Diffusion model.

        :param model_path: Local path to the Stable Diffusion model (including .safetensors file).
        """
        self.model_path = model_path
        self.pipe = self._load_model()

    def _load_model(self) -> StableDiffusion3Pipeline:
        """
        Loads the Stable Diffusion model.

        :return: The loaded model pipeline.
        """
        pipe = StableDiffusion3Pipeline.from_pretrained(self.model_path, use_safetensors=True, torch_dtype=torch.float32)
        
        # TODO: Add support for multiple devices (e.g., CUDA for GPU, DirectML for compatibility, and CPU as fallback)
        device = "cpu"
        pipe = pipe.to(device)

        return pipe

    def generate_image(self, prompt: str, width: int = 512, height: int = 512, num_inference_steps: int = 20, guidance_scale: float = 7.5) -> str:
        """
        Generates an image based on a text prompt using the Stable Diffusion model.

        :param prompt: Text prompt describing the image.
        :param output_path: (Optional) Path to save the generated image.
        :param width: Width of the generated image.
        :param height: Height of the generated image.
        :return: Base64-encoded image as a string.
        """
        # Generate the image
        image = self.pipe(prompt=prompt, num_inference_steps=num_inference_steps, guidance_scale=guidance_scale, height=height, width=width).images[0]

        # Convert the image to Base64
        buffered = BytesIO()
        image.save(buffered, format="PNG")
        img_base64 = base64.b64encode(buffered.getvalue()).decode("utf-8")

        return img_base64

if __name__ == "__main__":
    # Example usage
    model_path = "path\\to\\stable-diffusion-3.5-model"
    prompt = "A picturesque mountain landscape at sunset"

    # Optional path to save the image
    output_image_path = "C:\\temp\\image.png"
    width = 512
    height = 256

    image_generator = ImageGenerationService(model_path)

    print("Generating image...")
    base64_image = image_generator.generate_image(prompt, width=width, height=height)
    #ImageUtils.save_image_from_base64(base64_str=base64_image, output_image_path=output_image_path)

    print("Image has been generated and saved to:", output_image_path)
