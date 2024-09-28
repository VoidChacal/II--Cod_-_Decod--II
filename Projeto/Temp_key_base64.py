from cryptography.hazmat.primitives.ciphers import algorithms
from cryptography.hazmat.primitives.ciphers import Cipher
from cryptography.hazmat.primitives import padding
from cryptography.hazmat.backends import default_backend
import base64
import os

def generate_key_and_iv():
    # Gera uma chave e um IV adequados para AES
    key = os.urandom(16)  # 16 bytes = 128 bits
    iv = os.urandom(16)   # 16 bytes = 128 bits

    return base64.b64encode(key).decode('utf-8'), base64.b64encode(iv).decode('utf-8')

key, iv = generate_key_and_iv()

print("Chave em base64:", key)
print("IV em base64:", iv)
