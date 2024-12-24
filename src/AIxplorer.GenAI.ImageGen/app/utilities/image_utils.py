import base64

class ImageUtils:
    """
    Utility class for handling image-related operations.
    """

    @staticmethod
    def save_image_from_base64(base64_str: str, output_path: str) -> None:
        """
        Decodes a Base64-encoded image and saves it to a file.

        :param base64_str: The Base64-encoded image string.
        :param output_path: Path to save the decoded image.
        """
        image_data = base64.b64decode(base64_str)
        with open(output_path, "wb") as file:
            file.write(image_data)
