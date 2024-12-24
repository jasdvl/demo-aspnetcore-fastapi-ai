from app.api.image_generator import ImageGenerator
from config import MODEL_PATH, SERVER_HOST, SERVER_PORT
from contextlib import asynccontextmanager
from fastapi import FastAPI
from fastapi.middleware.cors import CORSMiddleware
from app.utilities.image_utils import ImageUtils

import base64
import os
import uvicorn

# Global resources
resources = {}

@asynccontextmanager
async def lifespan(app: FastAPI):
    """
    Lifespan function to initialize and clean up resources.
    """
    resources["image_generator"] = ImageGenerator(MODEL_PATH or "path/to/model")
    
    yield
    print("Cleaning up resources...")
    resources.clear()
    print("Resources cleaned up.")

app = FastAPI(lifespan=lifespan)

# CORS middleware
origins = [
    "http://localhost:4200"
]

app.add_middleware(
    CORSMiddleware,
    allow_origins=origins,
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

@app.get("/")
def read_root():
    return {"message": "Hello, ImageGen FastAPI!"}


@app.get("/generative-ai/image-generation")
def generate_image():
    image_generator: ImageGenerator = resources.get("image_generator")
    if not image_generator:
        raise RuntimeError("ImageGenerator is not initialized.")
    
    base64_image = image_generator.generate_image(("Kittens playing on a couch."))
    #ImageUtils.save_image_from_base64(base64_str=base64_image, output_image_path=output_image_path)
    
    return {"image": base64_image}

if __name__ == "__main__":
    uvicorn.run(app, host=SERVER_HOST, port=SERVER_PORT)
