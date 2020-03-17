
R = [100,200,500,800,900]
D=350

Itenerary=[]

for i in range(0,len(R)):
    if(len(Itenerary)!=0):
        dist_to_next_station=R[i]-R[Itenerary[len(Itenerary)-1]]
    else:
        dist_to_next_station=R[i]
    if(i+1<len(R)):
        if(dist_to_next_station+R[i+1]<=D):
            dist_to_next_station+=R[i+1]
            continue
    
    Itenerary.append(i)
    dist_to_next_station=0

print(Itenerary)