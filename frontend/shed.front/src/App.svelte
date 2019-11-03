<script>
  export let name;
  import Counter from "./Counter.svelte";
  import PostsList from "./PostsList.svelte";
</script>

<style>
  h1 {
    color: purple;
  }
</style>

<h1>Hello {name}!</h1>
<img
  src="https://image.shutterstock.com/image-photo/colorful-flower-on-dark-tropical-260nw-721703848.jpg"
  alt="coucou" />
<PostsList />
