<template>
  <div class="Registration Success">
    <h1>Registration success</h1>
    <p>
        account created, you have been logged in.
    </p>
  </div>
</template>

<script>
export default {
    methods:{
        registrationRedirect(){
            this.$router.push("/");
        }
    },
    beforeMount(){
        //i'd like to add a delay here.
        this.registrationRedirect()
    }
}
</script>
