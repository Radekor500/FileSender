import { InjectionKey } from 'vue'
import { AxiosInstance } from 'axios'
import FilesApi from './filesService'

export const AxiosKey: InjectionKey<FilesApi> = Symbol('http')
