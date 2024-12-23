from fastapi import FastAPI
from fastapi.middleware.cors import CORSMiddleware
from config import MODEL_PATH, SERVER_HOST, SERVER_PORT

import os
import uvicorn

app = FastAPI()

# CORS middleware
origins = [
    "http://localhost",
    "http://localhost:4200",
]

app.add_middleware(
    CORSMiddleware,
    allow_origins=origins,
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

modelpath = MODEL_PATH

@app.get("/")
def read_root():
    return {"message": "Hello, ImageGen FastAPI!"}


@app.get("/generative-ai/image-generation")
def generate_image():
    raise NotImplementedError("This method is not implemented yet.")

if __name__ == "__main__":
    
    # Start Uvicorn server on host/port
    uvicorn.run(app, host=SERVER_HOST, port=SERVER_PORT)

