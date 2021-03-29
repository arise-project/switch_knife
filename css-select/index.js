const listSelectors = require('list-css-selectors');
const { resolve } = require('path');

const pathToMyFile1 = resolve(resolve(), './asposeapp.css');
const selectors1 = listSelectors(pathToMyFile1);

const pathToMyFile2 = resolve(resolve(), './critical.css');
const selectors2 = listSelectors(pathToMyFile1);

let data = new Set();

for(val1 of selectors1)
{
    if(!data.has(val1))
    {
        for(val2 of selectors2)
        {
            if(val1 == val2)
            {
                data.add(val1);
            }
        }   
    }
}

var res = Array.from(data).sort();


for(r of res)
{
    console.log(r);
}