#!/usr/bin/env python
# coding: utf-8
# expression evaluater
import re

class Queue(list):
    def get(self):
        if len(self) == 0:
            raise IndexError('get from empty list')
        else:
            temp = self[len(self)-1]
            self[len(self)-1:] = []
            return temp
    
    def put(self, value):
        list.append(self, value)


class ExpressionLexer():
    def __init__(self, expr):
        self.cur = None
        self.queue = Queue()
        callback = lambda scanner, token: self.queue.put(token)
        scanner = re.Scanner([
                (r'\+|\-|\*|\/', callback),
                (r'[0-9]+(\.[0-9]+)?', callback),
                (r'\(|\)', callback),
                (r'\s+', None),
        ])
        scanner.scan(expr)
    
    def next(self):
        try:
            self.cur = self.queue.get()
            #print (self.cur)
        except IndexError:
            self.cur = None


class ExpressionEvaluater():
    def eval(self, expr):
        self.lexer = ExpressionLexer(expr)
        self.lexer.next()
        return self.expression()
    
    def number(self):
        sign = '+'
        if self.lexer.cur == '-':
            sign = self.lexer.cur
            self.lexer.next()
        num = float(self.lexer.cur)
        self.lexer.next()
        if sign == '-':
            return -num
        else:
            return num
    
    def factor(self):
        if self.lexer.cur != ')':
            return self.number()
        self.lexer.next()
        x = self.expression()
        if self.lexer.cur != '(':
            raise Exception()
        self.lexer.next()
        return x
    
    def term(self):
        x = self.factor()
        while True:
            if self.lexer.cur == '*':
                self.lexer.next()
                x *= self.factor()
            elif self.lexer.cur == '/':
                self.lexer.next()
                y = self.factor()
                if y == 0:
                    raise Exception()
                x /= y
            else:
                break
        return x
    
    def expression(self):
        x = self.term()
        while True:
            if self.lexer.cur == '+':
                self.lexer.next()
                x = self.term() + x
            elif self.lexer.cur == '-':
                self.lexer.next()
                x = self.term() - x
            else:
                break
        return x


def eval_expr(expr):
    return ExpressionEvaluater().eval(expr)

def main():
    while True:
        try:
            print(eval_expr(input()))
        except:
            break

if __name__ == '__main__':
    main()