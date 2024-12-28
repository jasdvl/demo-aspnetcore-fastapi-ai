from pydantic import BaseModel

class ImageGenerationRequest(BaseModel):
    prompt: str

class ImageGenerationResponse(BaseModel):
    # Base64-encoded image string
    base64_image: str
