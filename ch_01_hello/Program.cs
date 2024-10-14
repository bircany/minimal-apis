var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
string msg = "Hello";
User user = new User("Bircan","Yilmaz","bircanyilmaz1231@gmail.com","31241");


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/hello",() =>{
    return new Response(msg);
    //return msg + " " + user.Name + " " + user.Lastname + " " + user.Email + " " + user.Password + " " + user.GetCreationYear();
}).WithName("hello").WithOpenApi();


app.UseHttpsRedirection();


app.Run();
class Response {
    public Response(String msg)
    {
        Message = msg;
    }
    public String? Message { get; set; }
    public DateTime? Date => DateTime.Now;
}