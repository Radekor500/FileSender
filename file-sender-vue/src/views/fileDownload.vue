
<script setup lang="ts">
import { AxiosKey } from '@/shared/httpClient/symbo';
import { ref } from '@vue/reactivity';
import { onMounted } from '@vue/runtime-core';
import { inject, Ref } from 'vue';
import { useRoute } from 'vue-router';
import { FileDownloadModel } from '../shared/models/fileDownloadModel'

const http = inject(AxiosKey)!;
const files: Ref<FileDownloadModel[]> = ref([]);
const route = useRoute();

onMounted(async () => {
    try {
        let resp = await http.listAll(route.params.guid as string);
        files.value = resp.data;
    } catch (error) {
        alert(error)
    }
    
    

})



const generateLink = (response: any, filename: string): void  => {
    let dataType = response.type;
    let binaryData = [];
    binaryData.push(response);
    let downloadLink = document.createElement('a');
    downloadLink.href = window.URL.createObjectURL(new Blob(binaryData, {type: dataType}));
    if (filename)
          downloadLink.setAttribute('download', filename);
          document.body.appendChild(downloadLink);
          downloadLink.click();
  }

const downloadSingle = async (id: string, filename: string) => {
   let resp = await http.downloadSingleFile(id);
   generateLink(resp.data, filename);

}
</script>

<template>
    <h1>Hello</h1>
    <table>
            <thead>
                <tr>
                    <th>File name</th>
                    <th>Options</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="file in files" :key="file.fileId">
                    <td>{{file.fileName}}</td>
                    <td><button @click="downloadSingle(file.fileId, file.fileName)">Download</button></td>
                </tr>
            </tbody>
        </table>
</template>


<style lang="scss">

</style>
