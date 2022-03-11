import axios from "axios";
import { FileDownloadModel } from "../models/fileDownloadModel";
import { FileModel } from "../models/fileModel";

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
        return this.http.post<FileModel>("FileUpload/upload", data)
    }

    listAll(guid: string) {
        return this.http.get<FileDownloadModel>("FileUpload/listall", {params: {
            guid: guid
        }})
    }

    downloadSingleFile(guid: string) {
        return this.http.get("FileUpload/downloadsingle", 
        {params: {
            guid: guid
        }, responseType: "blob" as "json"})
    }

}

export default FilesApi