
function togglePassword(inputId)
{
    const passwordInput = document.getElementById(inputId);
    const toggleButton = passwordInput.nextElementSibling.querySelector('i');

    if (passwordInput.type === "password")
    {
        passwordInput.type = "text";
        toggleButton.classList.remove("bi-eye");
        toggleButton.classList.add("bi-eye-slash");
    }
    else
    {
        passwordInput.type = "password";
        toggleButton.classList.remove("bi-eye-slash");
        toggleButton.classList.add("bi-eye");
    }
 }
