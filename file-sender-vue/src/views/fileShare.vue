/* eslint-disable @typescript-eslint/no-non-null-assertion */
<template>
    <div class="form-wrap">
        <form @submit.prevent="onSubmit">>
            <input id="fileField" type="file" multiple required placeholder="input files">
            <input v-model="date" type="date">
            <button type="submit">Share files</button>
        </form>
    </div>
</template>

<script setup lang="ts">
import { ref } from "@vue/reactivity";
import { inject } from "@vue/runtime-core";
import { AxiosKey } from '../shared/httpClient/symbo';
//import FileService from './shared/httpClient/filesService'

const http = inject(AxiosKey)!;
const date = ref(new Date());
//const files = ref(null);

const onSubmit = async () => {
    const formData = new FormData();
    let files = document.getElementById('fileField')! as any;
    console.log(files)
    files = files.files;
    Object.keys(files).forEach(key => {
      formData.append("FileContent", files[key]);
    });
    formData.append("ExpiryDate", new Date(date.value).toISOString());
    let resp = await http.uploadFile(formData)
    alert("http://localhost:8080/files/" + resp.data.uploadId);
}
    
</script>

<style lang="scss">
    .form-wrap{
        grid-area: menu;
        width: 100%;
    }
</style>
