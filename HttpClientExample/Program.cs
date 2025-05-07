// See https://aka.ms/new-console-template for more information
using HttpClientExample;

Console.WriteLine("Hello, World!");

HttpClientexample example=new HttpClientexample();
//await example.Read();
//await example.Edit(1);
//await example.Edit(101);
await example.Create(20,"Text title","Text Body");