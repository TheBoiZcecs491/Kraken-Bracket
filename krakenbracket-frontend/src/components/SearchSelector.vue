<template>
    <div>
        <select class="primary" v-model="keyword">
            <option 
                v-for="option in options" 
                v-bind:key="option.text" 
                v-bind:value="option.value"
            >
                {{ option.text }}
            </option>
        </select>
    </div>
</template>

<script>
export default {
    name: "SearchSelector",
    data() {
        return {
            keyword: 'bracket',
            options: [
                {text: 'Brackets', value: 'bracket'},
                {text: 'Events', value: 'event'},
                {text: 'Gamers', value: 'gamer'}
            ]
        }
    },
}
</script>