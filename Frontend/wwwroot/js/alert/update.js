function update(uri, model, action) {
    swal.fire({
        title: 'Emin misiniz?',
        text: "Güncelleme işlemi yapmaktasınız Kaydı geri alamazsınız!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Evet, Güncelle!',
        cancelButtonText: 'Hayır,Güncelleme!',
        reverseButtons: true
    }).then(function (result) {
        if (result.value) {
            $.ajax({
                url: uri,
                type: 'POST',
                data: JSON.stringify(model),
                contentType: 'application/json',
                success: function (response) {
                    Swal.fire(
                        'Güncellendi!',
                        'Güncelleme Başarılı.',
                        'success'
                    ).then(() => {
                        window.location.href = action;
                    });
                },
                error: function () {
                    Swal.fire(
                        'Hata!',
                        'Güncellerken hata oluştu.',
                        'hata'
                    );
                }
            });
        } else if (
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swal.fire(
                'İptal',
                'Kayıtlarınız güvende:)',
                'hata'
            )
        }
    })
}
