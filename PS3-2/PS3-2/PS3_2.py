



def get_at_index(A, B, k):
    return get_recursive(A, 0, len(A)-1,B,0,len(B)-1, k )

def get_recursive(A, loA, hiA, B, loB, hiB, k):
    curr_A=[A[index] for index in range(loA,hiA+1)]
    curr_B=[B[index] for index in range(loB,hiB+1)]
    if(hiA < loA):
        return B[k-loA]
    if(hiB<loB):
        return A[k-loB]

    i=int((loA+hiA)/2)
    j=int((loB+hiB)/2)

    if(A[i]>B[j]):
        if(k<=i+j):
            return get_recursive(A,loA,i-1,B,loB,hiB,k)
        else:
            return get_recursive(A,loA,hiA,B,j+1,hiB,k)#element k cannot be B[j] or lower
    else:
        if(k<=i+j):
            return get_recursive(A,loA,hiA,B,loB,j-1,k)
        else:
            return get_recursive(A,i+1,hiA,B,loB,hiB,k)
    #    if (A[i] > B[j])
    #    if (k <= i+j)
    #        return select(A, $1, $2, B, $3, $4, k);
    #    else
    #        return select(A, $5, $6, B, $7, $8, k);           
    #else
    #    if (k <= i+j)
    #        return select(A, $9, $10, B, $11, $12, k);
    #    else
    #        return select(A, $13, $14, B, $15, $16, k);


G = [5,6,7,8]
H = [1,2,3,4]
print(get_at_index(G,H,7))
#wait = input("Press any key to exit...")


