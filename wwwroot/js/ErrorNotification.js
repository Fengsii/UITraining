    document.addEventListener("DOMContentLoaded", function () {
            // Cek apakah ada pesan error
            const errorMessage = '@TempData["ErrorMessage"]';
    if (errorMessage) {
        alert(errorMessage); // Tampilkan alert error
            }

    // Cek apakah ada pesan sukses
    const successMessage = '@TempData["SuccessMessage"]';
    if (successMessage) {
        alert(successMessage); // Tampilkan alert sukses
            }
        });
