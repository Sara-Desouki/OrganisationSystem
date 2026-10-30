namespace OrganisationSystem.APILayer
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        
        public string Message { get; set; }

        public T? Data { get; set; }

        public string Error { get; set; }

        public int Status { get; set; }


        public ApiResponse<T> Successed<T>(T DataResult)
        {
            return new ApiResponse<T>()
            {
                Success = true,
                Message = "Success Request",
                Data = DataResult,
                Error = null,
                Status = 200
            };
        }

        public ApiResponse<T> failed(string Error , int status ) {

            return new ApiResponse<T>() {
                Success = false,
                Message = "Failed Request", 
                Error = Error,
                Status = status
            
            
            };


        }
    }
}
