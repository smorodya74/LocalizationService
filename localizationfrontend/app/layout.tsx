'use client'
import '@ant-design/v5-patch-for-react-19';
import { Layout } from "antd";
import "./globals.css";
import { Footer } from "antd/es/layout/layout";

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body style={{ background: '#7566D1' }}>
        {children}
        <Footer
          style={{
            borderTop: '1px solid #404040',
            textAlign: "center",
            background: 'transparent',
            color: '#000000'
          }}
        >
          Localization Service © 2025 Created by Stepan Smorodnikov
        </Footer>
      </body>
    </html>
  );
}