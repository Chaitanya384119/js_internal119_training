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