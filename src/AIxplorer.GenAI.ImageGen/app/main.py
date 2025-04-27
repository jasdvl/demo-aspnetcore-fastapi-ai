from app.api.endpoints.image_generation import router as image_generation_router
from app.services.image_generation_service import ImageGenerationService
from app.core.resources import resources
from config import MODEL_PATH, SERVER_HOST, SERVER_PORT
from contextlib import asynccontextmanager
from fastapi import FastAPI
from fastapi.middleware.cors import CORSMiddleware

import base64
import os
import uvicorn

@asynccontextmanager
async def lifespan(app: FastAPI):
    """
    Lifespan function to initialize and clean up resources.
    """
    resources["image_generation_service"] = ImageGenerationService(MODEL_PATH)
    
    yield
    print("Cleaning up resources...")
    resources.clear()
    print("Resources cleaned up.")

app = FastAPI(lifespan=lifespan)

# CORS middleware
origins = [
    "http://localhost:6700",
    "http://localhost:10000",
    "http://host.docker.internal:10000"
]

app.add_middleware(
    CORSMiddleware,
    allow_origins=origins,
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

app.include_router(image_generation_router, prefix="/api")

@app.get("/")
def read_root():
    return {"message": "Hello, ImageGen FastAPI!"}

if __name__ == "__main__":
    uvicorn.run(app, host=SERVER_HOST, port=SERVER_PORT)
