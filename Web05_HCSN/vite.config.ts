import { fileURLToPath, URL } from 'node:url'
import Components from 'unplugin-vue-components/vite'
import AutoImport from 'unplugin-auto-import/vite'
import { ElementPlusResolver } from 'unplugin-vue-components/resolvers'
import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [
        vue({
            script: {
                defineModel: true
            }
        }),
        AutoImport({
            resolvers: [ElementPlusResolver()],
            eslintrc: { enabled: true },
            include: [
                /\.[tj]sx?$/, //Regex which says t or j followed by sx. //tsx, jsx, ? next to x says it can happen zero or one time js,ts
                /\.vue$/ //$ at the end says it ends with vue
            ],
            //globals (libraries)
            imports: ['vue', 'vue-router', 'pinia'],
            //other settings/files/dirs to import
            dts: true, //Autoimport all the files that ends with d.ts
            //Autoimport inside vue template
            vueTemplate: true
        }),
        Components({
            dirs: ['src/components'],
            dts: 'src/components.d.ts',
            resolvers: [ElementPlusResolver()],
            deep: true
        })
    ],
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url))
        }
    },
    css: {
        preprocessorOptions: {
            scss: {
                additionalData: `@import "@/scss/abstracts/mixin";`
            }
        }
    }
})
