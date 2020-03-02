import math

def ee(a, b):
    if b==0:
        return (1, 0, a)
    else:
        (x_alt,y_alt,d) = ee(b, a % b)
        return (y_alt, x_alt - int(a/b)*y_alt, d)



def inverse(a, N):
    (x, y, d) = ee(a, N)
    if d==1:
        return x % N
    else:
        return None


#for i in range(100):
#    if inverse(i, math.factorial(20)):
#        print("First integer with inverse mod 20! = "+ i)
#        break
#print("7^-1 mod 12= " + inverse(7,12))

#numInverses = 0
#for i in range(1, 124):
#    if inverse(i, 124):
#        numInverses+=1
#print("Number of positive integers with ")

for a in range(1,1000):
    for b in range(1,1000):
        if(inverse(a, b)):
            if(inverse(b,a)):
                continue
            else:
                print(False)
                break

