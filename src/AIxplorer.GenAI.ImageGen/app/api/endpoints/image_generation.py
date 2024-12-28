from app.core.resources import resources
from app.models.image_dto import ImageGenerationRequest, ImageGenerationResponse
from app.services.image_generation_service import ImageGenerationService
from fastapi import APIRouter, HTTPException

router = APIRouter()

@router.post("/generative-ai/image-generation", response_model=ImageGenerationResponse)
def generate_image(request: ImageGenerationRequest):
    """
    Generate an image based on the given prompt.
    """
    image_generation_service: ImageGenerationService = resources.get("image_generation_service")
    if not image_generation_service:
        raise HTTPException(status_code=500, detail="ImageGenerator is not initialized.")
    
    # Use the provided prompt to generate an image
    base64_image = image_generation_service.generate_image(request.prompt)
    
    return {"base64_image": base64_image}
