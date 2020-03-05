import math
import sys

def ee(a, b):
    if b==0:
        return (1, 0, a)
    else:
        (x_alt,y_alt,d) = ee(b, a % b)
        return (y_alt, x_alt - a//b * y_alt, d)

def inverse(a, N):
    (x, y, d) = ee(a, N)
    if d==1:
        return x % N
    else:
        return None

def isprime(N):
    a_list = [2,3,5]
    for a in a_list:
        if(exp(a,N-1,N) !=1):
            return False
            break
    return True

def gcd(a,b):
    while(b > 0):
        mod = a % b
        a = b
        b = mod
    return a

def exp(x, y, N):
    if(y == 0):
        return 1
    else:
        z = exp(x, y//2, N)
        if(y % 2 == 0):
            return z ** 2 % N
        else:
            return x * z ** 2 % N

def key(p, q):
    modulus = p*q
    phi = (p-1)*(q-1)
    e = 2
    while(gcd(e, phi) != 1):
        e += 1
    d = inverse(e, phi)
    return(modulus, e, d)


for l in sys.stdin:
    line = l.split()
    cmd = line[0]

    if(cmd == "isprime"):
        p = int(line[1])
        if(isprime(p)):
            print("yes")
        else:
            print("no")
    elif(cmd == "exp"):
        x = int(line[1])
        y = int(line[2])
        N = int(line[3])
        print(exp(x,y,N))
    else:
        arg1 = int(line[1])
        arg2 = int(line[2])
        if(cmd == "gcd"):
            print(gcd(arg1, arg2))
        elif(cmd == "inverse"):
            result = inverse(arg1,arg2)
            if(result != None):
                print(result)
            else:
                print("none")
        elif(cmd == "key"):
            to_print = []
            key_data = key(arg1,arg2)
            to_print.append(str(key_data[0]))
            to_print.append(" ")
            to_print.append(str(key_data[1]))
            to_print.append(" ")
            to_print.append(str(key_data[2]))
            print(''.join(to_print))

#input("press to exit...")









