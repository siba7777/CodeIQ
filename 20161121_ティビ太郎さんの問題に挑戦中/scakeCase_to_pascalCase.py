# coding: utf-8

import re

def snakeCase_to_pascalCase(input):
	words = input.split(",")
	for i in range(len(words)) :
		tmp = re.sub("^(.)",lambda x:x.group(0).upper(),words[i])
		words[i] = re.sub("_(.)",lambda x:x.group(1).upper(),tmp)
	#print(words)

	row1 = ""
	row2 = ""
	for word in words:
		row1 += word[0]
		row2 += word
	print(row1)
	print(row2)

def main():
	while True:
		try:
			snakeCase_to_pascalCase(input())
		except:
			break


if __name__ == '__main__':
    main()

