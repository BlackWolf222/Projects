import PDFMerger from 'pdf-merger-js';
import fsExtra from 'fs-extra'

const merger = new PDFMerger();

const mergePDFs = async (pdfFiles) => {
  fsExtra.emptyDir('public/merged')
  
  merger.reset();
  for (const pdfFile of pdfFiles) {
    await merger.add(pdfFile);
  }

  let d = new Date().getTime();
  let mergedFileName = `merged_${d}.pdf`;
  await merger.save(`public/merged/${mergedFileName}`);
  fsExtra.emptyDir('uploads');
  return mergedFileName;
}

export default mergePDFs;