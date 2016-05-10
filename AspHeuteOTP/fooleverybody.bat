del mysecret.pad
del ciphertext1.txt
del ciphertext2.txt
del plaindummy.txt

padgen mysecret.pad 200000
padxor 2encrypt.txt mysecret.pad 0 ciphertext1.txt
padxor ciphertext1.txt dummymessage.txt 0 ciphertext2.txt

padxor ciphertext2.txt ciphertext1.txt 0 plaindummy.txt