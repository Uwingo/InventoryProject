function deleted(uri, id, action) {
    swal.fire({
        title: 'Emin misiniz?',
        text: "Silme işlemi yapmaktasınız Kaydı geri alamazsınız!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Evet, Sil!',
        cancelButtonText: 'Hayır,Silme!',
        reverseButtons: true
    }).then(function (result) {
        if (result.value) {
            $.ajax({
                url: uri,
                type: 'POST',
                data: JSON.stringify(id),
                contentType: 'application/json',
                success: function (response) {
                    Swal.fire(
                        'Silindi!',
                        'Silme Başarılı.',
                        'success'
                    ).then(() => {
                        window.location.href = action;
                    });
                },
                error: function () {
                    Swal.fire(
                        'Hata!',
                        'Silerken hata oluştu.',
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