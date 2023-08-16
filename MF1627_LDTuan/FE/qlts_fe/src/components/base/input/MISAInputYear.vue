<template>
    <div class="input">
        <MISALabel label="Năm"></MISALabel>
        <div class="input__wrapper">
            <input type="text" value="2023">
            <div class="icon__wrapper">
                <MISAIcon pull_up></MISAIcon>
                <MISAIcon drop_down></MISAIcon>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    name: "MISAInputYear",
}
</script>
<style scoped>
.input{
    width: 112px;
    height: 30px;
    background-color: rgba(26, 164, 200, .2);
    box-sizing: border-box;
    border-radius: 3px;
    padding-right: 4px;
    margin-left: 22px;
    margin-right: 26px;
    display: flex;
    align-items: center;
}

.input .label{
    padding: unset;
    margin-left: 12px;
}

.input__wrapper{
    display: flex;
    position: relative;
    width: 100%;
    height: 100%;
    box-sizing: border-box;
}

.input__wrapper .icon{
    height: unset;
}

.input__wrapper input{
    border: none;
    background-color: unset;
    font-family: Roboto, sans-serif;
    font-weight: 700;
    width: 100%;
    padding-left: 14px;
    outline: unset;
}

.icon__wrapper{
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    row-gap: 4px;
    margin: 0 4px;
}
</style>