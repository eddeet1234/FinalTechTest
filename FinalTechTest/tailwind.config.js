/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./**/*.{razor,html,cshtml}"],
    safelist: [
        {
            pattern: /.*/, // Match all classes to include everything
        },
    ],
    theme: {
        extend: {
            colors: {
                'picton-blue': {
                    '50': '#f2f8fd',
                    '100': '#e4eefa',
                    '200': '#c3ddf4',
                    '300': '#8ec2eb',
                    '400': '#58a5df',
                    '500': '#2c87cb',
                    '600': '#1d6aac',
                    '700': '#18558c',
                    '800': '#184974',
                    '900': '#193d61',
                    '950': '#112740',
                },
            },

            fontFamily: {
                poppins: ["Poppins", "sans-serif"],
            },

        },
    },
    plugins: [],
}
