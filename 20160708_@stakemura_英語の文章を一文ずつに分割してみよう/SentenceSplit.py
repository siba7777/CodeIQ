# coding: UTF-8

import re

if __name__ == "__main__":

    try:
        while True:
            print(" \n".join(re.split("(?<!Mr[\.?!])(?<!Ms[\.?!])(?<!Mrs[\.?!])(?<!Mt[\.?!])(?<!\d[\.?!])(?<=[\.?!]) ", input())))
    except EOFError:
        pass