﻿function statistics() {
    $('#statistics_btn').on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();

        // hasClass('d-none') -> Statistics are hidden
        if ($('#statistics_box').hasClass('d-none')) {
            $.get('https://localhost:7271/api/statistics', function (data) {
                $('#total_animals').text(data.totalAnimals + " Animals");
                $('#total_adoptions').text(data.totalAdoptions + " Adoptions");

                $('#statistics_box').removeClass('d-none');

                $('#statistics_btn').text('Hide Statistics');
                $('#statistics_btn').removeClass('btn-primary');
                $('#statistics_btn').addClass('btn-danger');
            });
        } else {
            $('#statistics_box').addClass('d-none');

            $('#statistics_btn').text('Show Statistics');
            $('#statistics_btn').removeClass('btn-danger');
            $('#statistics_btn').addClass('btn-primary');
        }
    });
}