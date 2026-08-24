namespace Laboratory_Management_API.Extensions
{
    public static class SwaggerExtension
    {
        public static WebApplication AddSwaggerWithUI(this WebApplication app)
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }

    }
}
