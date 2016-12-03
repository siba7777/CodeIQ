# coding: utf-8
import binascii

def binary_to_char(binary_strings):
    ascii_string = ""
    binary_string_list = [binary_strings[i: i+8] for i in range(0, len(binary_strings), 8)]
    for binary_string in binary_string_list:
        ascii_string += chr(int(binary_string,2))
    return ascii_string

def main():
    while True:
        try:
            print(binary_to_char(input()))
        except:
            break

if __name__ == '__main__':
    main()