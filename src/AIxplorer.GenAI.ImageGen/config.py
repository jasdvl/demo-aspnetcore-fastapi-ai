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

DEFAULT_SERVER_HOST = "0.0.0.0"
DEFAULT_SERVER_PORT = 7600
# Path to Stable Diffusion model
RELATIVE_MODEL_PATH = "../../../../AI-Models/stable-diffusion-3.5-medium"

# Convert relative model path to absolute path
ABS_MODEL_PATH = os.path.abspath(RELATIVE_MODEL_PATH)


# Get environment variables, with default values if they are not set
MODEL_PATH = os.getenv("MODEL_PATH", ABS_MODEL_PATH)
SERVER_HOST = os.getenv("SERVER_HOST", DEFAULT_SERVER_HOST)
SERVER_PORT = int(os.getenv("SERVER_PORT", DEFAULT_SERVER_PORT))

