<script>
    function allvalidate() {
        var validated = true;
        if (!validate()) validated = false;
        if (!checkAge(age)) validated = false;
        if (!checkuser(email)) validated = false;

        return validated;
    }


    function validate() {
        var txtf = document.getElementById('name');
        if (txtf.value == 0 || txtf.value == null) {
            document.getElementById('error').innerText = ('you must enter firstname');
            document.getElementById('error').style.color = 'red';

            txtf.focus();
            return false;
        }
        else {
            document.getElementById('error').innerText = ('');
            return true;
        }
    }


    function checkAge(input) {
        if (input.value < 18 || input.value > 70) {
            document.getElementById('errorage').innerText = ('age must be from 18 :70');
            document.getElementById('errorage').style.color = 'red';
            return false;
        }
        else {
            document.getElementById('errorage').innerText = ('');
            return true;
        }

    }


    function checkuser(input) {
        var pattern = '^[a-zA-Z]+$';
        if (input.value.match(pattern)) {
            document.getElementById('erroruser').innerText = '';
            return true;
        }
        else {
            document.getElementById('erroruser').innerText = ('enter valid email');
            document.getElementById('erroruser').style.color = 'red';

            return false;
        }


    }



</script>