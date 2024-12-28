from app.utilities.image_utils import ImageUtils
from diffusers import StableDiffusion3Pipeline, StableDiffusion3Img2ImgPipeline
from pathlib import Path

import torch
# import torch_directml
from PIL import Image
from io import BytesIO
import base64
import sys

class Image2ImageGenerationService:
    def __init__(self, model_path: str):
            """
            Initializes the Image2ImageGenerator with a specific Stable Diffusion model.

            :param model_path: Local path to the Stable Diffusion model (including .safetensors file).
            """
            self.model_path = model_path
            self.pipe = self._load_model()

    def _load_model(self) -> StableDiffusion3Img2ImgPipeline:
        """
        Loads the Stable Diffusion model.

        :return: The loaded model pipeline.
        """
        pipe = StableDiffusion3Img2ImgPipeline.from_pretrained(self.model_path, use_safetensors=True, torch_dtype=torch.float32)
        
        # TODO: Add support for multiple devices (e.g., CUDA for GPU, DirectML for compatibility, and CPU as fallback)
        device = "cpu"
        pipe = pipe.to(device)

        return pipe

    def generate_image(
        self,
        prompt: str,
        init_image: Image,
        height: int = 512,
        width: int = 512,
        num_inference_steps: int = 20,
        strength: float = 0.7,
        guidance_scale: float = 7.5
    ) -> str:
        """
        Generates an image based on a given text prompt and an initial image using Stable Diffusion.

        :param prompt: Text prompt describing the image to generate.
        :param init_image: The initial image to guide the diffusion process.
        :param height: The height of the generated image (pixels).
        :param width: The width of the generated image (pixels).
        :param num_inference_steps: Number of inference steps for the diffusion model. Higher values may improve quality.
        :param strength: Controls how much the initial image influences the result (range: 0.0 to 1.0).
                         Lower values stick closely to the input image, while higher values allow for more transformation.
        :param guidance_scale: A higher value encourages the model to generate images closer to the prompt.
        :return: Base64-encoded PNG image as a string.
        """

        # Prepare the input image (the image must have a specific size)
        init_image = init_image.resize((width, height))

        # Generate the image
        image = self.pipe(prompt, image=init_image, num_inference_steps=num_inference_steps, strength=strength, guidance_scale=guidance_scale).images[0]

        # Convert the image to Base64
        buffered = BytesIO()
        image.save(buffered, format="PNG")
        img_base64 = base64.b64encode(buffered.getvalue()).decode("utf-8")

        return img_base64

if __name__ == "__main__":
    # Example usage
    # Path to the input JPG image
    init_image_path = "path\\to\\initial\\image"  
    init_image = Image.open(init_image_path)
    model_path = "path\\to\\stable-diffusion-3.5-model"
    prompt = "A picturesque mountain landscape at sunset"
    width = 512
    height = 256

    # Optional path to save the image
    output_image_path = "C:\\temp\\image.png"
    
    image_generator = Image2ImageGenerationService(model_path)

    print("Generating image...")
    base64_image = image_generator.generate_image(prompt, init_image=init_image, width=width, height=height)
    #ImageUtils.save_image_from_base64(base64_str=base64_image, output_image_path=output_image_path)

    print("Image has been generated and saved to:", output_image_path)
