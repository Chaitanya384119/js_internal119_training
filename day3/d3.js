function fun(n)
{
    return n*n;
}
let ch=fun(12)
console.log(ch)

arr=["mango","banana","guava","apple","melon"];;
let i=0
for(i=0;i<arr.length;i++)
{
    console.log (arr[i]);
}

arr.push("papaya")
for(let value of arr){
    console.log(value);
}
arr.pop();
for(let value of arr){
    console.log(value);
}



arr=[2,4,6,8]
let doubled=arr.map(n=>n*2)
for(let value of doubled){
    console.log(value);
}

arr=[10,25,30,5,60]
let res=arr.filter(n=>n>20)
for(let value of res){
    console.log(value);
}



arr=[1,2,3,4,5]
let total=arr.reduce((acc,val)=>1+val,0)

console.log(total);
