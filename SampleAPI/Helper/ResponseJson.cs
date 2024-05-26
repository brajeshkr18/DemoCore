namespace SampleAPI.Helper
{
    public class ResponseJson<T>
    {
        public int Code { get; set; }

        public string? Message { get; set; }

        public string? Message_key { get; set; }

        public T? Data { get; set; }

        public ResponseJson<T> GetOK(T data)
        {
            Code = 200;
            Data = data;
            Message = "OK";
            Message_key = "OK";
            return this;
        }

        public ResponseJson<T> GetOKDelete()
        {
            Code = 200;
            Message = "Register was deleted";
            Message_key = "OK";
            return this;
        }

        public ResponseJson<T> GetInternalServerError(Exception ex)
        {
            Code = 500;
            Message = "Internal Server Error - Try again in a few minutes or contact system administrator";
            Message_key = "Internal Server Error - " + ex.Message + " at " + ex.Source;
            return this;
        }

        public ResponseJson<T> GetInternalServerDetailsError(Exception ex)
        {
            Code = 500;
            Message = "Internal Server Error - Try again in a few minutes or contact system administrator";
            Message_key = "Internal Server Error - " + ex.Message + " at " + ex.StackTrace;
            return this;
        }

        public ResponseJson<T> GetNotFound()
        {
            Code = 404;
            Message = "Not Found, The provided id was not found, please check your URL";
            Message_key = "Not Found";
            return this;
        }

        public ResponseJson<T> GetBadRequestNull()
        {
            Code = 400;
            Message = "The provided object is null";
            Message_key = "Bad Request";
            return this;
        }

        public ResponseJson<T> GetBadRequest(T errors)
        {
            Code = 400;
            Message = "Bad Request";
            Message_key = "Bad Request";
            Data = errors;
            return this;
        }

        public ResponseJson<T> GetConflict(T errors)
        {
            Code = 409;
            Message = "Conflict";
            Message_key = "Conflict";
            Data = errors;
            return this;
        }

        public ResponseJson<T> GetNoData(T data)
        {
            Code = 204;
            Message = "Data not available for the request";
            Message_key = "Data Not Found";
            Data = data;
            return this;
        }

        public ResponseJson<T> GetWrongPassword(T errors)
        {
            Code = 401;
            Message = "Wrong Password";
            Message_key = "Wrong Password";
            Data = errors;
            return this;
        }

        public ResponseJson<T> GetInvalidEmail(T errors)
        {
            Code = 422;
            Message = "Invalid Email";
            Message_key = "Invalid Email";
            Data = errors;
            return this;
        }

        public ResponseJson<T> GetPasswordExpired(T errors)
        {
            Code = 401;
            Message = "Password Expired";
            Message_key = "Password Expired";
            Data = errors;
            return this;
        }

        public ResponseJson<T> GetUnauthorized(T errors)
        {
            Code = 401;
            Message = "Unauthorized";
            Message_key = "Unauthorized";
            Data = errors;
            return this;
        }
    }
}
