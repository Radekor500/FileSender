<template>
    <div class="form-wrap">
        <form @submit.prevent="onSubmit">>
            <input id="fileField" type="file" multiple required placeholder="input files">
            <input v-model="date" type="date">
            <button type="submit">Share files</button>
        </form>
    </div>
</template>

<script setup>
import { ref } from "@vue/reactivity";
import { inject } from "@vue/runtime-core";
import { AxiosKey } from '../shared/httpClient/symbo';
//import FileService from './shared/httpClient/filesService'

const http = inject(AxiosKey)
const date = ref(new Date());
//const files = ref(null);

const onSubmit = async () => {
    //console.log("works")
    //console.log(date);
    const formData = new FormData();
    let files = document.getElementById('fileField');
    console.log(files)
    files = files.files;
    Object.keys(files).forEach(key => {
      //console.log(files[key])
      formData.append("FileContent", files[key]);
    });
    formData.append("ExpiryDate", new Date(date.value).toISOString());
    //console.log(formData.getAll("FileContent"));
    // let resp = await fetch(`https://localhost:7033/API/FileUpload/upload`, {
    //     method: "POST",
    //     //headers: {'Content-Type': 'multipart/form-data'},
    //     body: formData

    // });
    
    // let data = await resp.json();
    let resp = await http.uploadFile(formData)
    console.log(resp)
}
    
</script>

<style lang="scss">
    .form-wrap{
        grid-area: menu;
        width: 100%;
    }
</style>
