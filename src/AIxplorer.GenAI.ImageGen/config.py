from dotenv import load_dotenv
import os


# Remove SERVER_HOST and SERVER_PORT from environment variables if they exist.
# This ensures that any previously set values (e.g., from the system or IDE) are cleared.
os.environ.pop("SERVER_HOST", None)
os.environ.pop("SERVER_PORT", None)

# Load environment variables from the .env file.
# The `override=True` option ensures that values from the .env file
# take precedence over existing environment variables.
load_dotenv(override=True)

# Default values

DEFAULT_SERVER_HOST = "127.0.0.1"
DEFAULT_SERVER_PORT = 7600

# Path to Stable Diffusion model
relative_model_path = os.getenv("MODEL_PATH")

if not relative_model_path:
    raise ValueError("MODEL_PATH is not set in the environment variables. Please set it in your .env file.")

# Get environment variables, with default values if they are not set
SERVER_HOST = os.getenv("SERVER_HOST", DEFAULT_SERVER_HOST)
SERVER_PORT = int(os.getenv("SERVER_PORT", DEFAULT_SERVER_PORT))

# Convert relative model path to absolute path
MODEL_PATH = os.path.abspath(relative_model_path)
