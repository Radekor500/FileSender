import { AxiosKey } from "@/shared/httpClient/symbo"
import { inject } from "vue"
import { Route } from "vue-route"

export const GuidResolver = async(to: Route) => {
    const http = inject(AxiosKey);
    to.meta
}