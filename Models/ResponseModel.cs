namespace orderform.Models;

public class ResponseModel<T>
{
    public T? respData { get; set; }
    public string respCode { get; set; } = null!;
    public string respMsg { get; set; } = null!;
}