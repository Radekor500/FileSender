import axios from "axios";

// const instance = axios.create({
//     baseURL: 'https://localhost:7033/API/',
//     timeout: 1000,
//     //headers: {'X-Custom-Header': 'foobar'}
//   });

class FilesApi {
    http;
    constructor() {
        this.http = axios.create({
            baseURL: 'https://localhost:7033/API/',
            timeout: 1000,
        })
    }

    uploadFile(data: FormData) {
        return this.http.post("FileUpload/upload", data)
    }
}

export default FilesApi