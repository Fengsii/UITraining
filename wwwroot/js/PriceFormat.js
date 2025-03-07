document.addEventListener("DOMContentLoaded", function () {
    let priceInputs = document.querySelectorAll('.price-format');

    // Format input ke Rupiah saat user mengetik
    priceInputs.forEach(function (input) {
        input.addEventListener('input', function (e) {
            let value = e.target.value.replace(/\D/g, ''); // Hanya angka
            let formatted = new Intl.NumberFormat('id-ID', {
                style: 'currency',
                currency: 'IDR',
                minimumFractionDigits: 0
            }).format(value);

            e.target.value = formatted; // Format sebagai Rupiah
        });
    });

    // Sebelum form dikirim ke server, ubah format Rupiah ke angka biasa
    document.querySelector("form").addEventListener("submit", function () {
        priceInputs.forEach(function (input) {
            input.value = input.value.replace(/[^0-9]/g, ""); // Hapus Rp dan titik
        });
    });
});
