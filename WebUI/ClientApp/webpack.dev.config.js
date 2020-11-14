const path = require('path');
const BundleTracker = require('webpack-bundle-tracker');
const VueLoaderPlugin = require('vue-loader/lib/plugin');

module.exports = {
    mode: 'development',
    context: __dirname,
    entry: './src/main.js',
    resolve: {
        modules: ['node_modules'],
        extensions: ['*', '.js', '.vue',],
        alias: {
            vue: 'vue/dist/vue.esm.js',
            app: path.resolve(__dirname, './src/')
        }
    },
    output: {
        path: path.resolve('../wwwroot/bundles/js/'),
        filename: '[name].js'
    },
    plugins: [
        new VueLoaderPlugin(),
        new BundleTracker({
            filename: './webpack-stats.json'
        })
    ],
    optimization: {
        minimize: false,
        splitChunks: {
            chunks: 'all',
            minSize: 32767,
            maxSize: 0,
            minChunks: 1,
            maxAsyncRequests: 4,
            maxInitialRequests: Infinity,
            automaticNameDelimiter: '~',
            name: true,
            cacheGroups: {
                devextreme: {
                    test: /[\\/]node_modules[\\/](devextreme|devextreme-vue)[\\/]/,
                    priority: -1,
                    name: 'devextreme'
                },
                vendors: {
                    test: /[\\/]node_modules[\\/]/,
                    priority: -2,
                    name: 'vendors'
                },
                default: {
                    minChunks: 2,
                    reuseExistingChunk: true
                }
            }
        }
    },
    // This will fix import of Devextreme theme CSS files.
    resolveLoader: {
        moduleExtensions: ['-loader']
    },
    module: {
        rules: [
            {
                test: /\.vue$/,
                include: path.resolve(__dirname, './src/'),
                loader: 'vue-loader'
            },
            {
                test: /\.(css|scss)$/,
                use: ['style-loader', 'css-loader','sass-loader']
            },
            {
                test: /\.(woff(2)?|ttf|eot|svg)(\?v=\d+\.\d+\.\d+)?$/,
                use: {
                    loader: 'file-loader',
                    options: {
                        name: '[hash]-[name].[ext]',
                        outputPath: '../fonts/',
                        publicPath: './bundles/fonts/'
                    }
                }
            },
            {
                test: /\.(png|jpg|gif)$/,
                use: {
                    loader: 'url-loader',
                    options: {
                        esModule: false,
                        limit: 2048,
                        name: '[hash]-[name].[ext]',
                        outputPath: '../img/',
                        publicPath: './bundles/img/'
                    }
                }
            }
        ]
    }
};
