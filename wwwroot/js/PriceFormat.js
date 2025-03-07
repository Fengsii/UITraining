document.addEventListener("DOMContentLoaded", function () {
    let priceInputs = document.querySelectorAll('.price-format'); // Ambil semua input dengan class ini

    priceInputs.forEach(function (input) {
        input.addEventListener('input', function (e) {
            let value = e.target.value.replace(/\D/g, ''); // Hanya angka
            let formatted = new Intl.NumberFormat('id-ID', {
                style: 'currency',
                currency: 'IDR',
                minimumFractionDigits: 0
            }).format(value);

            e.target.value = formatted; // Ubah tampilan input jadi format Rupiah
        });
    });
});
