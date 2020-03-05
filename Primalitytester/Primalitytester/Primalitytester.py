import math


def is_prime(N):
    for a in range(2, N):
        if(math.pow(a, N-1) % N == 1):
            return True

if(2 ** ((2 ** 23498273492739427489237423742892) - 1) % 2 == 1):
        print(True)
else:
    print(False)