/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./src/**/*.{html,ts}",
    ],
    theme: {
        extend: {
            zIndex: {
                '500': '500',
                '510': '510',
            },
            colors: {
                'primary': '#f4f2e9', 
                'secondary': '#495057',
                'header': '#2e65a0',
                'footer': '#2e65a0',
                'menu': '#496582',
                'dark': '#343a40',
                'light': '#e9ecef',
            },
        },
    },
    plugins: [],
}
