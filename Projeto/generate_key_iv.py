from Crypto.Cipher import AES
from Crypto.Random import get_random_bytes
import base64

# Gerar uma chave e um IV
key = get_random_bytes(16)  # 16 bytes para AES-128
iv = get_random_bytes(16)   # 16 bytes para AES

# Codificar em base64 para facilitar a visualização e uso
key_b64 = base64.b64encode(key).decode()
iv_b64 = base64.b64encode(iv).decode()

print("Chave em base64:", key_b64)
print("IV em base64:", iv_b64)
