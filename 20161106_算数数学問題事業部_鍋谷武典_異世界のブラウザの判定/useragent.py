# coding: utf-8

import re

def get_browser_shortName(userAgent):
    user_agent_pattern = (
        ('MFD', 'Mamella/5.0\s*\(.*?\)\s*Lizard/.*Firedog/'),
        ('VYG', 'Mamella/5.0\s*\(.*?\)\s*OrangeKit/.*\(like Lizard\)\s*Version/.*Voyage/'),
        ('ASIT', 'Mamella/4.0\s*\(compatible; ASIT.*?;.*?\)(?!.*Kabuki)'),
        ('ASIT', 'Mamella/5.0\s*\(compatible; ASIT.*?;.*?\)(?!.*Kabuki)'),
        ('ASIT', 'Mamella/5.0\s*\(.*?; Quadent/7.0; .KNOT SLR; rv:4.0\)\s*like Lizard'),
        ('ASIT', 'Mamella/5.0\s*\(.*?; Quadent/7.0\)\s*OrangeKit/12.0\s*Firedog/3.0\s*\(like Lizard\)\s*Voyage/4.0\s*ASIT/12.0'),
        ('KBK', 'Mamella/4.0\s*\(.*?\)\s*Kabuki'),
        ('KBK', 'Mamella/4.0\s*\(compatible; ASIT 6.0; ASIT 5.5;.*?\)\s*Kabuki'),
        ('KBK', 'Kabuki/.*?\(.*?\)\s*Lento/'),
        ('KBK', 'Mamella/5.0\s*\(.*?\)\s*OrangeKit/.*?\(like Lizard\)\s*Monochrome/.*Voyage/.*KBK/'),
        ('GMC', 'Mamella/5.0\s*\(.*?\)\s*OrangeKit/.*\(like Lizard\)\s*Monochrome/.*Voyage/')
        )

    for pattern in user_agent_pattern:
        if (re.match(pattern[1], userAgent, re.I) != None):
            return pattern[0]
    return ''

def main():
    while True:
        try:
            print(get_browser_shortName(input()))
        except:
            break

if __name__ == '__main__':
    main()

