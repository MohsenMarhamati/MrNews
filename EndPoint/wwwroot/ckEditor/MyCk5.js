ClassicEditor
    .create(document.querySelector('.editor'), {
        // Editor configuration.
        //پشتیبانی از تگ های HTML
        htmlSupport: {
            allow: [
                {
                    name: /.*/,
                    attributes: true,
                    classes: true,
                    styles: true
                }
            ]
        },
        //edit toolbar
        toolbar: {
            items: [
                'undo', 'redo',
                '|', 'heading',
                '|', 'fontColor', 'fontsize', 'fontBackgroundColor',
                '|', 'bold', 'italic', 'code',
               /* '|', 'uploadImage', 'imageInsert', 'mediaEmbed',*/
                '|', 'link', 'blockQuote', 'codeBlock',
                '|', 'alignment',
                '|', 'bulletedList', 'numberedList', 'outdent', 'indent'
/*                '|', 'htmlEmbed', 'sourceEditing'*/
            ],
            shouldNotGroupWhenFull: true
        },
        //edit heading
        heading: {
            options: [
                { model: 'paragraph', title: 'Paragraph', class: 'ck-heading_paragraph' },
                { model: 'heading1', view: 'h1', title: 'Heading 1', class: 'ck-heading_heading1' },
                { model: 'heading2', view: 'h2', title: 'Heading 2', class: 'ck-heading_heading2' },
                { model: 'heading3', view: 'h3', title: 'Heading 3', class: 'ck-heading_heading3' },
                { model: 'heading4', view: 'h4', title: 'Heading 4', class: 'ck-heading_heading4' },
                { model: 'heading5', view: 'h5', title: 'Heading 5', class: 'ck-heading_heading5' },
                { model: 'heading6', view: 'h6', title: 'Heading 6', class: 'ck-heading_heading6' },
            ]
        },
        //edit link
        link: {
            addTargetToExternalLinks: true,
            decorators: [
                { mode: 'manual', label: 'قابلیت دانلود', attributes: { download: 'download' }, defaultValue: false, },
            ],
        },
        //edit font color
        fontColor: {
            colors: [
                { color: 'red', label: 'قرمز' },
                { color: 'green', label: 'سبز' },
                { color: 'blue', label: 'آبی' },
                { color: 'pink', label: 'صورتی' },
                { color: '#f7f7f7', label: 'خاکستری' },
            ]
        },
        //edit background color
        fontBackgroundColor: {
            colors: [
                { color: 'red', label: 'قرمز' },
                { color: 'green', label: 'سبز' },
                { color: 'blue', label: 'آبی' },
                { color: 'pink', label: 'صورتی' },
            ]
        },
    })
    .then(editor => {
        window.editor = editor;
    })
    .catch(handleSampleError);
function handleSampleError(error) {
    const issueUrl = 'https://github.com/ckeditor/ckeditor5/issues';
    const message = [
        'Oops, something went wrong!',
        `Please, report the following error on ${issueUrl} with the build id "npwdoukyfz5-3bd6as171pns" and the error stack trace:`
    ].join('\n');
}